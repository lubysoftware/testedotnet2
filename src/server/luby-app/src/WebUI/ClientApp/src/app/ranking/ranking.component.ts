import { Component } from '@angular/core'; 
import { DesenvolvedorRankingClient, RankingDto } from '../web-api-client';

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.css']
})
export class RankingComponent {
  rankingVM: RankingDto[];

  constructor(private client: DesenvolvedorRankingClient) {
    this.client.getRankingDesenvolvedor(null).subscribe(result => {
      this.rankingVM = result;
    }, error => console.error(error));
  }
}
