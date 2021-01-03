import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DeveloperService } from 'src/app/shared/services/developer.service';
import { DeveloperRankingListDto } from '../dtos/ranking/developer-ranking-list.dto';
import { DeveloperRankingSearchDto } from '../dtos/ranking/developer-ranking-search.dto';

@Component({
  selector: 'app-developer-ranking',
  templateUrl: './developer-ranking.component.html',
  styleUrls: ['./developer-ranking.component.scss']
})
export class DeveloperRankingComponent implements OnInit {

  developersRanking: DeveloperRankingListDto[];

  constructor(
    private readonly developerService: DeveloperService,
    private readonly snackBar: MatSnackBar
  ) { 
    this.loadRanking();
  }

  ngOnInit(): void {
  
  }

  loadRanking() {
    this.developerService.ranking({} as DeveloperRankingSearchDto).subscribe(result => {
      if (!result.success) {
        this.snackBar.open('Falha ao carregar o ranking dos desenvolvedores!', 'OK', { duration: 3000 });
        return ;
      }
      this.developersRanking = result.data;
    }, () => {
      this.snackBar.open('Falha ao carregar o ranking dos desenvolvedores!', 'OK', { duration: 3000 });
    });
  }
}
