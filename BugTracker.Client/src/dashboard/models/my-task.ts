import {EmployeeShortData} from "./employee-short-data";

export interface MyTask {
  id: number;
  name: string;
  category: string;
  priority: string;
  status: string;
  assignerId: number;
  assigner: EmployeeShortData;
  assigneeId: number;
  assignee: EmployeeShortData;
}
