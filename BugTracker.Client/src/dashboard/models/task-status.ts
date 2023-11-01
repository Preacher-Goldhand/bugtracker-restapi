export type TaskStatus = 'TO_DO';

export const TaskStatus = {
  TO_DO: 'TO_DO' as TaskStatus,
  IN_PROGRESS: 'IN_PROGRESS' as TaskStatus,
  IN_TESTING: 'IN_TESTING' as TaskStatus,
  DONE: 'DONE' as TaskStatus,
  CLOSED: 'CLOSED' as TaskStatus
}
