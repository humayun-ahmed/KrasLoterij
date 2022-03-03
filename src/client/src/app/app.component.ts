import {Component, OnInit} from '@angular/core';
import {AppService} from "./app.service";
import {ScratchModel} from "./scratch.model";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Kras Loterij';
  isScratched=false;
  userId='';
  scratches: ScratchModel[] = [];
  constructor(private appService: AppService) {
    this.userId = this.appService.getUserId();
  }

  ngOnInit(): void {
    this.getData();
  }

  getData(){
    this.appService.getData().subscribe((scratches: any) => {
      this.scratches = scratches;
    })
  }

  scratch(index: number){
    console.log("isScratched is "+this.isScratched)
    if(!this.isScratched)
    {
      this.appService.isScratchedByUser().subscribe((isScratchedServer: any) => {
        this.isScratched = isScratchedServer;
        if(!this.isScratched)
        {
          this.scratches[index].userId = this.appService.getUserId();
          this.appService.updateData(this.scratches[index]).subscribe(()=>{})
          this.isScratched = true;
        }
      })
    }
  }

  counter(i: number) {
    return new Array(i);
  }
}
