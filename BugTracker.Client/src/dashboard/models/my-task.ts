import {EmployeeShortData} from "./employee-short-data";

export interface MyTask {
  id: number;
  boardId: number;
  name: string;
  category: string;
  priority: string;
  status: string;
  assignerId: number;
  assigner: EmployeeShortData;
  assigneeId: number;
  assignee: EmployeeShortData;
  loggedTime: number;
  storyPoints: number;
  proposalDate: Date;
  description: string;
}
