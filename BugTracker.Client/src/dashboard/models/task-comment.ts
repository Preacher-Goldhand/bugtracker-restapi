import {Employee} from "./employee";

export interface TaskComment {
  id?: number;
  taskId?: number;
  description: string;
  dateOfCreation: Date;
  userCreatedId: number;
  userCreated?: Employee;
}
