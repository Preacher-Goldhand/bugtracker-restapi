export interface Task {
  id?: number;
  name: string;
  description: string;
  category: string;
  dateOfCreation: Date;
  proposalDate: Date;
  loggedTime: number;
  priority: number;
  storyPoints: number,
  taskStatus: string;
  assignerId: number;
  assigneeName: string;
  boardId: number;
}
