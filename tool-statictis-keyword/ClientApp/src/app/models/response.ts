export class Response<TData> {
    actionState: ActionState;
    message: string;
    data: TData;
}

export enum ActionState {
    Success,
    Warning,
    Error
}
