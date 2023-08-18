import { MinimalQuestData } from "./minimal-quest.model";

export interface DetailedBoardData {
  name: string;
  dateOfCreation: Date;
  boardTasks: MinimalQuestData[];
}
