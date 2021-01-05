export class Keyword{
    selected: boolean;
    id: number;
    name: string;
    note: string;
    campaignId: number;
    constructor() {
        this.selected = false;
    }
}
export class FilterKeywordDto {
    name: string;
    note: string;
    campaignId: number;
    pageNumber: number;
    pageSize: number;
}
export class AddKeywordDto {
    name: string;
    note: string;
    campaignId: number;
}
export class EditKeywordDto {
    id: number;
    name: string;
    note: string;
    campaignId: number;
}
export class EditRangeKeyworDto{
    ids: number[];
    campaignId: number;
    note: string;
}
