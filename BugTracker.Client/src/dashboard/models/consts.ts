import {TaskStatus} from "./task-status";
import {TaskCategory} from "./task-category";
import {TaskPriority} from "./task-priority";

export const TaskStatusesMap = new Map<string, string>()
  .set(TaskStatus.TO_DO, "ToDo");

export const TaskCategoriesMap = new Map()
  .set(TaskCategory.DEVELOPMENT_TASK, "Development Task");

export const TaskPrioritiesMap = new Map()
  .set(TaskPriority.LOW, "Low")
  .set(TaskPriority.HIGH, "High")
  .set(TaskPriority.CRITICAL, "Critical");
