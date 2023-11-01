import {TaskStatus} from "./task-status";
import {TaskCategory} from "./task-category";
import {TaskPriority} from "./task-priority";

export const TaskStatusesMap = new Map<string, string>()
  .set(TaskStatus.TO_DO, "To Do")
  .set(TaskStatus.IN_PROGRESS, "In Progress")
  .set(TaskStatus.IN_TESTING, "In Testing")
  .set(TaskStatus.DONE, "Done")
  .set(TaskStatus.CLOSED, "Closed");

export const TaskCategoriesMap = new Map()
  .set(TaskCategory.ADMINISTRATIVE_TASK, "Administrative Task")
  .set(TaskCategory.ANALYTIC_TASK, "Analytic Task")
  .set(TaskCategory.BUGFIX_TASK, "Bugfix Task")
  .set(TaskCategory.DEVELOPMENT_TASK, "Development Task")
  .set(TaskCategory.DEVOPS_TASK, "DevOps Task")
  .set(TaskCategory.TESTING_TASK, "Testing Task");

export const TaskPrioritiesMap = new Map()
  .set(TaskPriority.LOW, "Low")
  .set(TaskPriority.HIGH, "High")
  .set(TaskPriority.CRITICAL, "Critical");
