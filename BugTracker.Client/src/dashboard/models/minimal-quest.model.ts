import {Employee} from "./employee";

export interface MinimalQuestData {
  id: number;
  name: string;
  category: string;
  priority: number;
  taskStatus: string;
  assigner: Employee;
  assignee: Employee;
}
