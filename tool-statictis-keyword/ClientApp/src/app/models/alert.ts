export enum AlertType{
    success,
    info,
    warning,
    error
}
export class Alert{
    type: string;
    message: string;
    timeout: boolean;
    constructor(type: AlertType, message: string, timeout: number = 10000){
        switch(type){
            case AlertType.error:
                this.type = 'danger';
                break;
            case AlertType.warning:
                this.type = 'warning';
                break;
            case AlertType.info:
                this.type = 'info';
                break;
            case AlertType.success:
                this.type = 'success';
                break;
            default:
                this.type = 'warning';
                break;
        }
        this.timeout = false;
        this.message = message;
        setTimeout(()=> {this.timeout = true;}, timeout);
    }
}