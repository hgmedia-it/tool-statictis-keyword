import { DatePipe, formatDate } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModuleMapNgFactoryLoader } from '@nguniversal/module-map-ngfactory-loader';
import { Alert, AlertType } from 'src/app/models/alert';
import { Page } from 'src/app/models/page';
import { ActionState } from 'src/app/models/response';
import { AddCampaignDto, Campaign, EditCampaignDto, FilterCampaignDto } from './campaign.model';
import { CampaignService } from './campaign.service';

@Component({
  selector: 'app-campaign',
  templateUrl: './campaign.component.html',
  providers: [CampaignService, DatePipe]
})
export class CampaignComponent implements OnInit {
    pageCampaign: Page<Campaign>;
    selectedAll: boolean;
    shiftSelect_CurrentIndex: number;
    shiftSelect_KeyPressing: boolean;
    //dto
    filters: FilterCampaignDto;
    addDto: AddCampaignDto;
    editDto: EditCampaignDto;
    deleteDto : number[];
    modalAlerts: Alert[];
    mainAlerts: Alert[];
    constructor(private campaignService: CampaignService, private modalService: NgbModal){
      this.pageCampaign = new Page<Campaign>();
      this.filters = new FilterCampaignDto();
      this.addDto = new AddCampaignDto();
      this.editDto = new EditCampaignDto();
      this.modalAlerts = new Array<Alert>();
      this.mainAlerts = new Array<Alert>();
      this.getCampaigns();

    }
    ngOnInit() {
    }
    getCampaigns(){
      this.selectedAll = false;
      this.pageCampaign.items = new Array<Campaign>();
      this.filters.pageNumber = this.pageCampaign.pageNumber;
      this.filters.pageSize = this.pageCampaign.pageSize;     
      const alert = new Alert(AlertType.info, 'Đang lấy dữ liệu campaign...', 999 * 1000);
      this.mainAlerts.push(alert);
      this.campaignService.get(this.filters).subscribe(successRes => {
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
      this.getCampaigns();
    }
    countSelectedItem() {
      return this.pageCampaign.items.filter(item => item.selected === true).length;
    }
    selectItem(item: any) {
      if (this.shiftSelect_KeyPressing) {
        const prevIndex = this.shiftSelect_CurrentIndex;
        this.shiftSelect_CurrentIndex = this.pageCampaign.items.indexOf(item);
        const fromIndex = prevIndex < this.shiftSelect_CurrentIndex ? prevIndex : this.shiftSelect_CurrentIndex;
        const toIndex = prevIndex > this.shiftSelect_CurrentIndex ? prevIndex : this.shiftSelect_CurrentIndex;
        const selectOrUnselect = this.pageCampaign.items[prevIndex].selected;
  
        for (let i = fromIndex; i <= toIndex; i++) {
          this.pageCampaign.items[i].selected = selectOrUnselect;
        }
      } else {
        this.shiftSelect_CurrentIndex = this.pageCampaign.items.indexOf(item);
      }
    }
    selectAll(){
      if(this.selectedAll == true){
        this.pageCampaign.items.forEach(item => {item.selected = false;});
      }else{
        this.pageCampaign.items.forEach(item => {item.selected = true;});
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
        this.addDto = new AddCampaignDto();
      }
      this.modalService.open(modal, {size: 'xl', backdrop: 'static', keyboard: false, scrollable: true}).result.then(() => {
        this.getCampaigns();
      }, () => {
        this.getCampaigns();
      });
    }
    openDeleteModal(modal){
      if(this.countSelectedItem() === 0){
        this.mainAlerts.push(new Alert(AlertType.warning, 'Select at least 1 campaign to delete'));
        return;
      }else{
        this.deleteDto = this.pageCampaign.items.filter(i=> i.selected).map(campaign => campaign.id);
      }
      this.modalService.open(modal, {size: 'xl', backdrop: 'static', keyboard: false, scrollable: true}).result.then(() => {
        this.getCampaigns();
      }, () => {
        this.getCampaigns();
      });
    }
    openEditModal(modal){
      if(this.countSelectedItem() === 0){
        this.mainAlerts.push(new Alert(AlertType.warning, 'Select 1 campaign to edit'));
        return;
      }else if(this.countSelectedItem() >1){
        this.mainAlerts.push(new Alert(AlertType.warning, 'Select only 1 campaign to edit'));
        return;
      }
      else{
        const editCampaign = this.pageCampaign.items.filter(i=> i.selected)[0];
        this.editDto = {
          id: editCampaign.id,
          name: editCampaign.name,
          description: editCampaign.description,
          startDate: editCampaign.startDate,
          endDate: editCampaign.endDate
        }
      }
      this.modalService.open(modal, {size: 'xl', backdrop: 'static', keyboard: false, scrollable: true}).result.then(() => {
        this.getCampaigns();
      }, () => {
        this.getCampaigns();
      });
    }
    addCampaign(){
      this.modalAlerts = [];
      if(typeof this.addDto.name == 'undefined' || this.addDto.name === ""){
        this.modalAlerts.push(new Alert(AlertType.warning, 'Name can not be null'));
      }else if(typeof this.addDto.startDate == 'undefined'){
        this.modalAlerts.push(new Alert(AlertType.warning, 'Start date invalid'));
        return;
      }else if(typeof this.addDto.endDate == 'undefined'){
        this.modalAlerts.push(new Alert(AlertType.warning, 'End date invalid'));
        return;
      }else{
        this.campaignService.add(this.addDto).subscribe(successRes => {
          if (successRes.actionState === ActionState.Error) {
              this.modalAlerts.push(new Alert(AlertType.error, 'Error when add new campaign: ' + successRes.message));
          } else if (successRes.actionState === ActionState.Warning) {
              this.modalAlerts.push(new Alert(AlertType.warning, 'Warning when add new campaign: ' + successRes.message));
          } else {
              this.modalAlerts.push(new Alert(AlertType.success, 'Add new campaign successfully'));
          }
      }, errorRes => {
          this.modalAlerts.push(new Alert(AlertType.error, 'Error when add new campaign: ' + errorRes.message));
      });
      }

    }
    closeAlert(alert: Alert, alerts: Alert[]) {
      alerts.splice(alerts.indexOf(alert), 1);
  }
    getCampaignFromId(id: number) {
      return this.pageCampaign.items.find(item => item.id === id);
    }
    deleteCampaign(){
      this.modalAlerts = [];
      this.campaignService.delete(this.deleteDto).subscribe(successRes => {
        if (successRes.actionState === ActionState.Error) {
            this.modalAlerts.push(new Alert(AlertType.error, 'Error when delete campaign: ' + successRes.message));
        } else if (successRes.actionState === ActionState.Warning) {
            this.modalAlerts.push(new Alert(AlertType.warning, 'Warning when delete campaign: ' + successRes.message));
        } else {
            this.modalAlerts.push(new Alert(AlertType.success, 'Delete campaign successfully'));
        }
      }, errorRes => {
        this.modalAlerts.push(new Alert(AlertType.error, 'Error when delete campaign: ' + errorRes.message));
      });
    }
}
