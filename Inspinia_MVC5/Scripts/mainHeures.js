



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


loadJSON("ParametrageJRS",
    function (data) {

        //(function dynamicTaskType() {
        //    var delTaskParent;

        //    function checkParents(id) {
        //        setTaskType(id);
        //        var parent = gantt.getParent(id);
        //        if (parent != gantt.config.root_id) {
        //            checkParents(parent);
        //        }
        //    }

        //    function setTaskType(id) {
        //        id = id.id ? id.id : id;
        //        var task = gantt.getTask(id);
        //        var type = gantt.hasChild(task.id) ? gantt.config.types.project : gantt.config.types.task;
        //        if (type != task.type) {
        //            task.type = type;
        //            gantt.updateTask(id);
        //        }
        //    }

        //    gantt.attachEvent("onParse", function () {
        //        gantt.eachTask(function (task) {
        //            setTaskType(task);
        //        });
        //    });

        //    gantt.attachEvent("onAfterTaskAdd", function onAfterTaskAdd(id) {
        //        gantt.batchUpdate(checkParents(id));
        //    });

        //    gantt.attachEvent("onBeforeTaskDelete", function onBeforeTaskDelete(id, task) {
        //        delTaskParent = gantt.getParent(id);
        //        return true;
        //    });

        //    gantt.attachEvent("onAfterTaskDelete", function onAfterTaskDelete(id, task) {
        //        if (delTaskParent != gantt.config.root_id) {
        //            gantt.batchUpdate(checkParents(delTaskParent));
        //        }
        //    });

        //})();

        // recalculate progress of summary tasks when the progress of subtasks changes
        //(function dynamicProgress() {

        //    function calculateSummaryProgress(task) {
        //        if (task.type != gantt.config.types.project)
        //            return task.progress;
        //        var totalToDo = 0;
        //        var totalDone = 0;
        //        gantt.eachTask(function (child) {
        //            if (child.type != gantt.config.types.project) {
        //                totalToDo += child.duration;
        //                totalDone += (child.progress || 0) * child.duration;
        //            }
        //        }, task.id);
        //        if (!totalToDo) return 0;
        //        else return totalDone / totalToDo;
        //    }

        //    function refreshSummaryProgress(id, submit) {
        //        if (!gantt.isTaskExists(id))
        //            return;

        //        var task = gantt.getTask(id);
        //        task.progress = calculateSummaryProgress(task);

        //        if (!submit) {
        //            gantt.refreshTask(id);
        //        } else {
        //            gantt.updateTask(id);
        //        }

        //        if (!submit && gantt.getParent(id) !== gantt.config.root_id) {
        //            refreshSummaryProgress(gantt.getParent(id), submit);
        //        }
        //    }


        //    gantt.attachEvent("onParse", function () {
        //        gantt.eachTask(function (task) {
        //            task.progress = calculateSummaryProgress(task);
        //        });
        //    });

        //    gantt.attachEvent("onAfterTaskUpdate", function (id) {
        //        refreshSummaryProgress(gantt.getParent(id), true);
        //    });

        //    gantt.attachEvent("onTaskDrag", function (id) {
        //        refreshSummaryProgress(gantt.getParent(id), false);
        //    });
        //    gantt.attachEvent("onAfterTaskAdd", function (id) {
        //        refreshSummaryProgress(gantt.getParent(id), true);
        //    });


        //    (function () {
        //        var idParentBeforeDeleteTask = 0;
        //        gantt.attachEvent("onBeforeTaskDelete", function (id) {
        //            idParentBeforeDeleteTask = gantt.getParent(id);
        //        });
        //        gantt.attachEvent("onAfterTaskDelete", function () {
        //            refreshSummaryProgress(idParentBeforeDeleteTask, true);
        //        });
        //    })();
        //})();


        gantt.config.work_time = true;

        for (var i = 0; i < data.length; i++) {
            gantt.setWorkTime(data[i]);
        }

        //gantt.setWorkTime(
        //    {   
        //        day:2,
        //        hours: [8, 12, 13, 17]
        //    },
        //    {
        //        day: 3,
        //        hours: [8, 11, 13, 17]
        //    },
        //    {
        //        day: 4,
        //        hours: [8, 10, 13, 17]
        //    }
        //);//global working hours. 8:00-12:00, 13:00-17:00

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



        gantt.templates.progress_text = function (start, end, task) {
            return "<span style='text-align:left;'>" + Math.round(task.progress * 100) + "% </span>";
        };

        gantt.templates.task_class = function (start, end, task) {
            if (task.type == gantt.config.types.project)
                return "hide_project_progress_drag";
        };
        gantt.config.xml_date = "%Y-%m-%d %H:%i:%s"; // format of dates in XML

        gantt.init("ganttContainer");
        gantt.load("/GanttDiag/DataHeures", "json");

        var dp = new dataProcessor("/GanttDiag/SaveHeures");
        dp.init(gantt);
    },
    function (xhr) { /*console.error(xhr);*/ }
);