import {Component, OnInit} from '@angular/core';
import {AppService} from "./app.service";
import {LotteryModel} from "./lotteryModel";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Kras Loterij';
  isScratched=false;
  userId='';
  message='';
  lotteries: LotteryModel[] = [];
  constructor(private appService: AppService) {
    this.userId = this.appService.getUserId();
  }

  ngOnInit(): void {
    this.getData();
  }

  getData(){
    this.appService.getData().subscribe((lotteries: any) => {
      this.lotteries = lotteries;
        console.log("Http get called.")
    },
      (error) => {                              //Error callback
        this.errorHandle(error);
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
          this.lotteries[index].userId = this.appService.getUserId();
          this.appService.updateData(this.lotteries[index]).subscribe(()=>{
            console.log("Http scratch called.")
            },
            (error) => {                              //Error callback
              this.errorHandle(error);
            })
          this.isScratched = true;
        }
      },
        (error) => {                              //Error callback
          this.errorHandle(error);
        })
    }
    if(this.isScratched)
    {
      this.message='Sorry!!! You already scratched.';
    }
  }
  errorHandle(error:any)
  {
    console.error(error);
    this.message='Oops!!! Something is wrong.';
  }

  counter(i: number) {
    return new Array(i);
  }
}
