import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpParams, HttpRequest } from '@angular/common/http';
import { Page } from 'src/app/models/page';
import { Response } from '../../models/response';

@Injectable()
export class VideoService {
    private httpClient: HttpClient;
    private url: string;
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.httpClient = http;
        this.url = baseUrl + 'api/video';
    }
    add(dto: number[]){
        const url = this.url + '/add';
        return this.httpClient.post<Response<string>>(url,dto);
    }
}