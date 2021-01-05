export class Video{
    selected: boolean;
    id: number;
    videoId: string;
    channelName: string;
    channelId: number;
    publishDate: Date;
    isLive: boolean;
    constructor() {
        this.selected = false;
    }
}
export class FilterVideoDto{
    videoId: string;
    channelId: string;
    channelName: number;
    publishDate: Date;
    keyword: string;
    pageNumber: number;
    pageSize: number;
}
export class AddVideoDto {
    name: string;
    note: string;
    campaignId: number;
}
export class EditVideoDto {
    id: number;
    name: string;
    note: string;
    campaignId: number;
}
