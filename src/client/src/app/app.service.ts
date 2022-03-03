import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class AppService{
  constructor(private http: HttpClient) {
  }

  getData(){
    return this.http.get('https://localhost:5001/api/Scratch');
  }

  updateData(data: any){
    return this.http.put('https://localhost:5001/api/Scratch', data);
  }
}
