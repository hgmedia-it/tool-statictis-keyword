import { DatePipe, formatDate } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Alert, AlertType } from 'src/app/models/alert';
import { Page } from 'src/app/models/page';
import { ActionState } from 'src/app/models/response';
import { Campaign, FilterCampaignDto } from '../campaign/campaign.model';
import { CampaignService } from '../campaign/campaign.service';
import { Video } from './video.model';
import { VideoService } from './video.service';

@Component({
  selector: 'app-video',
  templateUrl: './video.component.html',
  providers: [VideoService, DatePipe,CampaignService]
})
export class VideoComponent implements OnInit {
    // pageVideo: Page<Video>;
    // pageCampaign: Page<Campaign>;
    // selectedAll: boolean;
    // shiftSelect_CurrentIndex: number;
    // shiftSelect_KeyPressing: boolean;
    // //dto
    // filters: FilterKeywordDto;
    // filterCampaign: FilterCampaignDto;
    // addDto: AddKeywordDto;
    // editDto: EditKeywordDto;
    // editRangeDto: EditRangeKeyworDto;
    // deleteDto : number[];
    // modalAlerts: Alert[];
    // mainAlerts: Alert[];
    constructor(private videoService: VideoService, private modalService: NgbModal, private campaignService: CampaignService){
      // this.pageKeyword = new Page<Keyword>();
      // this.pageCampaign = new Page<Campaign>();
      // this.filters = new FilterKeywordDto();
      // this.filters.campaignId = 0;
      // this.filterCampaign = new FilterCampaignDto();
      // this.addDto = new AddKeywordDto();
      // this.addDto.campaignId = 1;
      // this.editDto = new EditKeywordDto;
      // this.editRangeDto = new EditRangeKeyworDto;
      // this.modalAlerts = new Array<Alert>();
      // this.mainAlerts = new Array<Alert>();
      // this.getKeywords();
      // this.getCampaigns();

    }
    ngOnInit() {
    }
 
}
