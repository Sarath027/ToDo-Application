export interface UserTask {
    taskId : number,
    title : string,
    description : string,
    status : string,
    createdOn : Date,
    completedOn? : Date
}
