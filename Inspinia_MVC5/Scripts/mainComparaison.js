gantt.config.xml_date = "%Y-%m-%d %H:%i:%s";
gantt.config.task_height = 16;
gantt.config.row_height = 40;
gantt.locale.labels.baseline_enable_button = 'Set';
gantt.locale.labels.baseline_disable_button = 'Remove';


function showAll() {
    gantt.ignore_time = null;
    gantt.render();
}

function hideWeekEnds() {
    gantt.ignore_time = function (date) {
        return !gantt.isWorkTime(date, "day");
    };
    gantt.render();
}

function hideNotWorkingTime() {
    gantt.ignore_time = function (date) {
        return !gantt.isWorkTime(date);
    };
    gantt.render();
}


function loadJSON(path, success, error) {
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            if (xhr.status === 200) {
                if (success)
                    success(JSON.parse(xhr.responseText));
            } else {
                if (error)
                    error(xhr);
            }
        }
    };
    xhr.open("GET", path, true);
    xhr.send();
}

//////////////////////

loadJSON("ParametrageJRS",
    function (data) {
        //gantt.serverList("staff", data);
        //gantt.locale.labels.section_owner = "Owner";

        function byId(list, id) {
            for (var i = 0; i < list.length; i++) {
                if (list[i].key == id)
                    return list[i].label || "";
            }
            return "";
        }
        gantt.config.columns = [
            { name: "text", label: "Name", tree: true, width: '*' },
            //{
                //name: "owner",/* width: 80,*/ label: "Owner", align: "center", template: function (item) {
                //    if (item.type == gantt.config.types.project) {
                //        return "";
                //    }

                //    return byId(gantt.serverList('staff'), item.owner_id)
                //}
           // },
            { name: "start_date",/* width: 70,*/ label: "Start", align: "center" },
            { name: "end_date", width: 70, label: "End", align: "center", template: function (task) { return gantt.templates.date_grid(task.end_date) } },
            {
                name: "duration", width: 50, label: "Duration", align: "center", template: function (task) {
                    var res = 0;
                    if (task.type != gantt.config.types.project) {
                        res = +task.duration;
                    } else {
                        res = gantt.getSubtaskDuration(task.id);
                    }
                    return res + "h"
                }
            },
            //{ name: "add", width: 40 }
        ];
        /////////////////////////////


        for (var i = 0; i < data.length; i++) {
            gantt.setWorkTime(data[i]);
        }
        gantt.config.work_time = true;
        gantt.config.scale_unit = "day";
        gantt.config.date_scale = "%l, %F %d";
        gantt.config.min_column_width = 20;
        gantt.config.duration_unit = "hour";
        gantt.config.scale_height = 20 * 3;

        gantt.config.auto_scheduling = true;
        gantt.config.auto_scheduling_strict = true;


        gantt.templates.task_cell_class = function (task, date) {
            var css = [];

            //if (date.getHours() == 7) {
            //    css.push("day_start");
            //}
            //if (date.getHours() == 16) {
            //    css.push("day_end");
            //}
            //if (!gantt.isWorkTime(date, 'day')) {
            //    css.push("week_end");
            //}
            //else
            if (!gantt.isWorkTime(date, 'hour')) {
                css.push("no_work_hour");
            }

            return css.join(" ");
        };


        var weekScaleTemplate = function (date) {
            var dateToStr = gantt.date.date_to_str("%d %M");
            var weekNum = gantt.date.date_to_str("(week %W)");
            var endDate = gantt.date.add(gantt.date.add(date, 1, "week"), -1, "day");
            return dateToStr(date) + " - " + dateToStr(endDate) + " " + weekNum(date);
        };

        gantt.config.subscales = [
            { unit: "week", step: 1, template: weekScaleTemplate },
            { unit: "hour", step: 1, date: "%G" }

        ];


        gantt.config.lightbox.sections = [
            { name: "description", height: 70, map_to: "text", type: "textarea", focus: true },
            { name: "time", map_to: "auto", type: "duration" },
            {
                name: "baseline",
                map_to: { start_date: "planned_start", end_date: "planned_end" },
                button: true,
                type: "duration_optional"
            }
        ];
        gantt.locale.labels.section_baseline = "Planned";



        gantt.attachEvent("onAfterTaskUpdate", function (id) {
            gantt.refreshData();
        });
        gantt.attachEvent("onAfterTaskAdd", function (id) {
            gantt.refreshData();
        });
        gantt.attachEvent("onAfterSelect", function (id) {
            gantt.refreshData();
        });
        // adding baseline display
        gantt.addTaskLayer(function draw_planned(task) {
            if (task.planned_start && task.planned_end) {
                var sizes = gantt.getTaskPosition(task, task.planned_start, task.planned_end);
                var el = document.createElement('div');
                el.className = 'baseline';
                el.style.left = sizes.left + 'px';
                el.style.width = sizes.width + 'px';
                el.style.top = sizes.top + gantt.config.task_height + 13 + 'px';
                return el;
            }
            return false;
        });
        gantt.templates.progress_text = function (start, end, task) {
            return "<span style='text-align:left;'>" + Math.round(task.progress * 100) + "% </span>";
        };
        gantt.templates.task_class = function (start, end, task) {
            if (task.planned_end) {
                var classes = ['has-baseline'];
                if (end.getTime() > task.planned_end.getTime()) {
                    classes.push('overdue');
                }
                return classes.join(' ');
            }
        };

        gantt.templates.rightside_text = function (start, end, task) {
            if (task.planned_end) {
                if (end.getTime() > task.planned_end.getTime()) {
                    //var overdue = Math.ceil(Math.abs((end.getTime() - task.planned_end.getTime()) / (24 * 60 * 60 * 1000)));
                    var overdue = task.retard;
                    var text = "<b>Retard: " + overdue + " heures</b>";
                    return text;
                }
            }
        };


        /////////////////////////////////////////////////
        gantt.config.work_time = true;
        gantt.skip_off_time = true;
        /////////////////////////////////////////////////




        gantt.attachEvent("onTaskLoading", function (task) {
            task.planned_start = gantt.date.parseDate(task.planned_start, "xml_date");
            task.planned_end = gantt.date.parseDate(task.planned_end, "xml_date");
            return true;
        });

        gantt.init("ganttContainer");
        gantt.load("/GanttDiag/DataComparaison");
        var dp = new dataProcessor("/GanttDiag/SaveComparaison");
        dp.init(gantt);
    },
    function (xhr) { /*console.error(xhr);*/ }
);



