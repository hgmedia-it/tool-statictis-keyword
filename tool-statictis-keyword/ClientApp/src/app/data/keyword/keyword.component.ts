import { DatePipe, formatDate } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Alert, AlertType } from 'src/app/models/alert';
import { Page } from 'src/app/models/page';
import { ActionState } from 'src/app/models/response';
import { Campaign, FilterCampaignDto } from '../campaign/campaign.model';
import { CampaignService } from '../campaign/campaign.service';
import { VideoService } from '../video/video.service';
import { AddKeywordDto, EditKeywordDto, EditRangeKeyworDto, FilterKeywordDto, Keyword } from './keyword.model';
import { KeywordService } from './keyword.service';

@Component({
  selector: 'app-keyword',
  templateUrl: './keyword.component.html',
  providers: [KeywordService, DatePipe,CampaignService,VideoService]
})
export class KeywordComponent implements OnInit {
    pageKeyword: Page<Keyword>;
    pageCampaign: Page<Campaign>;
    selectedAll: boolean;
    shiftSelect_CurrentIndex: number;
    shiftSelect_KeyPressing: boolean;
    //dto
    filters: FilterKeywordDto;
    filterCampaign: FilterCampaignDto;
    addDto: AddKeywordDto;
    editDto: EditKeywordDto;
    editRangeDto: EditRangeKeyworDto;
    deleteDto : number[];
    modalAlerts: Alert[];
    mainAlerts: Alert[];
    constructor(private keywordService: KeywordService, private videoService: VideoService,private modalService: NgbModal, private campaignService: CampaignService){
      this.pageKeyword = new Page<Keyword>();
      this.pageCampaign = new Page<Campaign>();
      this.filters = new FilterKeywordDto();
      this.filters.campaignId = 0;
      this.filterCampaign = new FilterCampaignDto();
      this.addDto = new AddKeywordDto();
      this.addDto.campaignId = 1;
      this.editDto = new EditKeywordDto;
      this.editRangeDto = new EditRangeKeyworDto;
      this.modalAlerts = new Array<Alert>();
      this.mainAlerts = new Array<Alert>();
      this.getKeywords();
      this.getCampaigns();

    }
    ngOnInit() {
    }
    getKeywords(){
      this.selectedAll = false;
      this.pageKeyword.items = new Array<Keyword>();   
      this.filters.pageNumber = this.pageKeyword.pageNumber;
      this.filters.pageSize = this.pageKeyword.pageSize; 
      const alert = new Alert(AlertType.info, 'Đang lấy dữ liệu keyword...', 999 * 1000);
      this.mainAlerts.push(alert);
      this.keywordService.get(this.filters).subscribe(successRes => {
          alert.timeout = true;
          if (successRes.actionState === ActionState.Error) {
              this.mainAlerts.push(new Alert(AlertType.error, 'Có lỗi khi lấy dữ liệu keyword: ' + successRes.message));
          } else if (successRes.actionState === ActionState.Warning) {
              this.mainAlerts.push(new Alert(AlertType.warning, 'Cảnh báo khi lấy dữ liệu keyword: ' + successRes.message));
          } else {
              this.pageKeyword = successRes.data;
          }
      }, errorRes => {
          alert.timeout = true;
          this.mainAlerts.push(new Alert(AlertType.error, 'Có lỗi khi lấy dữ liệu keyword: ' + errorRes.message));
      });
    }
    getCampaigns(){
      this.selectedAll = false;
      this.pageCampaign.items = new Array<Campaign>();   
      const alert = new Alert(AlertType.info, 'Đang lấy dữ liệu campaign...', 999 * 1000);
      this.mainAlerts.push(alert);
      this.campaignService.get(this.filterCampaign).subscribe(successRes => {
          alert.timeout = true;
          if (successRes.actionState === ActionState.Error) {
              this.mainAlerts.push(new Alert(AlertType.error, 'Có lỗi khi lấy dữ liệu campaign: ' + successRes.message));
          } else if (successRes.actionState === ActionState.Warning) {
              this.mainAlerts.push(new Alert(AlertType.warning, 'Cảnh báo khi lấy dữ liệu campaign: ' + successRes.message));
          } else {
              this.pageCampaign = successRes.data;
              this.pageCampaign.items.forEach(element => {
                element.startDate = formatDate(element.startDate,'yyyy-MM-dd', 'en-US');
                element.endDate = formatDate(element.endDate,'yyyy-MM-dd', 'en-US');
              });
          }
      }, errorRes => {
          alert.timeout = true;
          this.mainAlerts.push(new Alert(AlertType.error, 'Có lỗi khi lấy dữ liệu campaign: ' + errorRes.message));
      });
    }
    changePage(){
      this.getKeywords();
    }
    countSelectedItem() {
      return this.pageKeyword.items.filter(item => item.selected === true).length;
    }
    selectItem(item: any) {
      if (this.shiftSelect_KeyPressing) {
        const prevIndex = this.shiftSelect_CurrentIndex;
        this.shiftSelect_CurrentIndex = this.pageKeyword.items.indexOf(item);
        const fromIndex = prevIndex < this.shiftSelect_CurrentIndex ? prevIndex : this.shiftSelect_CurrentIndex;
        const toIndex = prevIndex > this.shiftSelect_CurrentIndex ? prevIndex : this.shiftSelect_CurrentIndex;
        const selectOrUnselect = this.pageKeyword.items[prevIndex].selected;
  
        for (let i = fromIndex; i <= toIndex; i++) {
          this.pageKeyword.items[i].selected = selectOrUnselect;
        }
      } else {
        this.shiftSelect_CurrentIndex = this.pageKeyword.items.indexOf(item);
      }
    }
    selectAll(){
      if(this.selectedAll == true){
        this.pageKeyword.items.forEach(item => {item.selected = false;});
      }else{
        this.pageKeyword.items.forEach(item => {item.selected = true;});
      }
    }
    @HostListener('window:keydown', ['$event'])
    onKeydown(event) {
      if (event.key === 'Shift') {
        this.shiftSelect_KeyPressing = true;
      }
    }
    @HostListener('window:keyup', ['$event'])
    onKeyup(event) {
      if (event.key === 'Shift') {
        this.shiftSelect_KeyPressing = false;
      }
    }
    openAddModal(modal){
      if (!this.addDto) {
        this.addDto = new AddKeywordDto();
      }
      this.modalService.open(modal, {size: 'xl', backdrop: 'static', keyboard: false, scrollable: true}).result.then(() => {
        this.getKeywords();
      }, () => {
        this.getKeywords();
      });
    }
    openDeleteModal(modal){
      if(this.countSelectedItem() === 0){
        this.mainAlerts.push(new Alert(AlertType.warning, 'Select at least 1 campaign to delete'));
        return;
      }else{
        this.deleteDto = this.pageKeyword.items.filter(i=> i.selected).map(keyword => keyword.id);
      }
      this.modalService.open(modal, {size: 'xl', backdrop: 'static', keyboard: false, scrollable: true}).result.then(() => {
        this.getKeywords();
      }, () => {
        this.getKeywords();
      });
    }
    openEditModal(modal){
      if(this.countSelectedItem() === 0){
        this.mainAlerts.push(new Alert(AlertType.warning, 'Select at least 1 campaign to edit'));
        return;
      }else if(this.countSelectedItem() ===1){
        let item = this.pageKeyword.items.find(i=> i.selected);
        this.editDto.id = item.id;
        this.editDto.campaignId = item.campaignId;
        this.editDto.name = item.name;
        this.editDto.note = item.note;
      }else if(this.countSelectedItem() >1){
        this.editRangeDto.campaignId = 1;
        this.editRangeDto.note = "";
        let item = this.pageKeyword.items.filter(i=> i.selected).map(keyword => keyword.id);
        this.editRangeDto.ids = item;
      }
      this.modalService.open(modal, {size: 'xl', backdrop: 'static', keyboard: false, scrollable: true}).result.then(() => {
        this.getKeywords();
      }, () => {
        this.getKeywords();
      });
    }
    addKeywords(){
      if(typeof this.addDto.name == 'undefined' || this.addDto.name === "" ){
        this.modalAlerts.push(new Alert(AlertType.warning, 'Name can not be null or empty'));
        return;
      }
      this.keywordService.add(this.addDto).subscribe(
        successRes => {
          if (successRes.actionState === ActionState.Error) {
              this.modalAlerts.push(new Alert(AlertType.error, 'Error when add new keywords: ' + successRes.message));
          } else if (successRes.actionState === ActionState.Warning) {
              this.modalAlerts.push(new Alert(AlertType.warning, 'Warning when add new keywords: ' + successRes.message));
          } else {
              this.modalAlerts.push(new Alert(AlertType.success, 'Add new keywords successfully'));
          }
        }, errorRes => {
          this.modalAlerts.push(new Alert(AlertType.error, 'Error when add new keywords: ' + errorRes.message));
        });
    }
    updateTopVideoForKeyword(){
      if(this.countSelectedItem() === 0){
        this.mainAlerts.push(new Alert(AlertType.warning, 'Select at least 1 campaign to edit'));
        return;
      }else{
        this.deleteDto = this.pageKeyword.items.filter(i=> i.selected).map(keyword => keyword.id);
        this.videoService.add(this.deleteDto).subscribe(
          successRes => {
            if (successRes.actionState === ActionState.Error) {
                this.mainAlerts.push(new Alert(AlertType.error, 'Error when update top video for keyword: ' + successRes.message));
            } else if (successRes.actionState === ActionState.Warning) {
                this.mainAlerts.push(new Alert(AlertType.warning, 'Warning when update top video for keyword: ' + successRes.message));
            } else {
                this.mainAlerts.push(new Alert(AlertType.success, 'Update top video for keyword successfully'));
            }
          }, errorRes => {
            this.mainAlerts.push(new Alert(AlertType.error, 'Error when Update top video for keyword: ' + errorRes.message));
          });
      }
    }
    closeAlert(alert: Alert, alerts: Alert[]) {
      alerts.splice(alerts.indexOf(alert), 1);
  }
    getCampaignFromId(id: number) {
      return this.pageCampaign.items.find(item => item.id === id);
    }
    getKeywordFromId(id: number) {
      return this.pageKeyword.items.find(item => item.id === id);
    }
    deleteKeyword(){
      this.modalAlerts = [];
      this.keywordService.delete(this.deleteDto).subscribe(successRes => {
        if (successRes.actionState === ActionState.Error) {
            this.modalAlerts.push(new Alert(AlertType.error, 'Error when delete keyword: ' + successRes.message));
        } else if (successRes.actionState === ActionState.Warning) {
            this.modalAlerts.push(new Alert(AlertType.warning, 'Warning when delete keyword: ' + successRes.message));
        } else {
            this.modalAlerts.push(new Alert(AlertType.success, 'Delete keyword successfully'));
        }
      }, errorRes => {
        this.modalAlerts.push(new Alert(AlertType.error, 'Error when delete keyword: ' + errorRes.message));
      });
    }
    editKeyword(){
      this.modalAlerts = [];
      if(this.countSelectedItem() ===1){
        this.keywordService.edit(this.editDto).subscribe(successRes => {
          if (successRes.actionState === ActionState.Error) {
              this.modalAlerts.push(new Alert(AlertType.error, 'Error when edit keyword: ' + successRes.message));
          } else if (successRes.actionState === ActionState.Warning) {
              this.modalAlerts.push(new Alert(AlertType.warning, 'Warning when edit keyword: ' + successRes.message));
          } else {
              this.modalAlerts.push(new Alert(AlertType.success, 'edit keyword successfully'));
          }
        }, errorRes => {
          this.modalAlerts.push(new Alert(AlertType.error, 'Error when edit keyword: ' + errorRes.message));
        });
      }else{
        this.keywordService.editRange(this.editRangeDto).subscribe(successRes => {
          if (successRes.actionState === ActionState.Error) {
              this.modalAlerts.push(new Alert(AlertType.error, 'Error when edit keyword: ' + successRes.message));
          } else if (successRes.actionState === ActionState.Warning) {
              this.modalAlerts.push(new Alert(AlertType.warning, 'Warning when edit keyword: ' + successRes.message));
          } else {
              this.modalAlerts.push(new Alert(AlertType.success, 'edit keyword successfully'));
          }
        }, errorRes => {
          this.modalAlerts.push(new Alert(AlertType.error, 'Error when edit keyword: ' + errorRes.message));
        });
      }
    }
}
