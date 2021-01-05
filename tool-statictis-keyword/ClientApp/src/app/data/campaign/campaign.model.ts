export class Campaign{
    selected: boolean;
    id: number;
    name: string;
    description: string;
    startDate: string;
    endDate: string;
    constructor() {
        this.selected = false;
    }
}
export class FilterCampaignDto {
    name: string;
    description: string;
    startDate: Date;
    endDate: Date;
    pageNumber: number;
    pageSize: number;
}
export class AddCampaignDto {
    name: string;
    description: string;
    startDate: Date;
    endDate: Date;
}
export class EditCampaignDto {
    id: number;
    name: string;
    description: string;
    startDate: string;
    endDate: string;
}