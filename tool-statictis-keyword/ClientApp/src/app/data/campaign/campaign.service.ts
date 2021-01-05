import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpParams, HttpRequest } from '@angular/common/http';
import { AddCampaignDto, Campaign, FilterCampaignDto } from './campaign.model';
import { Page } from 'src/app/models/page';
import { Response } from './../../models/response';

@Injectable()
export class CampaignService {
    private httpClient: HttpClient;
    private url: string;
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.httpClient = http;
        this.url = baseUrl + 'api/campaign';
    }
    get(dto: FilterCampaignDto){
        const url = this.url + '/get';
        return this.httpClient.post<Response<Page<Campaign>>>(url, dto);
    }
    add(dto: AddCampaignDto){
        return this.httpClient.post<Response<Page<Campaign>>>(this.url,dto);
    }
    delete(ids: number[]){
        return this.httpClient.request<Response<number>>('DELETE', this.url, { body: ids });
    }
}