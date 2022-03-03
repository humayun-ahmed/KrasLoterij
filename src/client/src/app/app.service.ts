import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {LotteryModel} from "./lotteryModel";

@Injectable({
  providedIn: 'root'
})
export class AppService{
  private readonly userId: string = this.newGuid();
  constructor(private http: HttpClient) {
  }

  getData(){
    return this.http.get('http://localhost:5000/api/v1/Get');
  }

  updateData(data: any){
    return this.http.put('http://localhost:5000/api/v1/Scratch', data);
  }
  isScratchedByUser(){
    return this.http.get('http://localhost:5000/api/v1/IsScratchedByUser?userId='+ this.getUserId());
  }
  getUserId(){
    return this.userId;
  }
  private newGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      var r = Math.random() * 16 | 0,
        v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }
}
