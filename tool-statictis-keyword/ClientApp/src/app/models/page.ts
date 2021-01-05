export class Page<T>{
    pageNumber: number;
    pageSize: number;
    itemCount: number;
    pageCount: number;
    items: T[];
    constructor(){
        this.pageNumber = 1;
        this.pageSize = 50;
        this.itemCount = 0;
        this.pageCount = 0;
        this.items = new Array<T>();
    }
}