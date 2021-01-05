import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpParams, HttpRequest } from '@angular/common/http';
import { Page } from 'src/app/models/page';
import { Response } from './../../models/response';
import { AddKeywordDto, EditKeywordDto, EditRangeKeyworDto, FilterKeywordDto, Keyword } from './keyword.model';

@Injectable()
export class KeywordService {
    private httpClient: HttpClient;
    private url: string;
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.httpClient = http;
        this.url = baseUrl + 'api/keyword';
    }
    get(dto: FilterKeywordDto){
        const url = this.url + '/get';
        return this.httpClient.post<Response<Page<Keyword>>>(url, dto);
    }
    add(dto: AddKeywordDto){
        return this.httpClient.post<Response<Page<Keyword>>>(this.url,dto);
    }
    delete(ids: number[]){
        return this.httpClient.request<Response<number>>('DELETE', this.url, { body: ids });
    }
    edit(dto: EditKeywordDto){
        return this.httpClient.put<Response<string>>(this.url,dto);
    }
    editRange(dto: EditRangeKeyworDto){
        const url = this.url + '/edit-range';
        return this.httpClient.put<Response<string>>(this.url,dto);
    }
}