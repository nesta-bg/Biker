import { Component, OnInit } from '@angular/core';
import { ChartType, ChartOptions } from 'chart.js';
import { SingleDataSet, Label, monkeyPatchChartJsLegend, monkeyPatchChartJsTooltip } from 'ng2-charts';
import { BikeService } from '../services/bike.service';
import { BarChartData } from '../models/barChartData';

@Component({
  selector: 'pie-chart',
  templateUrl: './pie-chart.component.html'
})
export class PieChartComponent implements OnInit {
  makeNames = [];
  items = [];

  public pieChartOptions: ChartOptions = {
    responsive: true,
    title: {
      display: true,
      text: 'Currently Available Bikes:',
      fontColor: '#fff',
      fontSize: 36   
    },
    legend: {
      display: true,
      labels: {
        fontColor: '#fff', 
      },
    }
  };
  public pieChartLabels: Label[] = this.makeNames;
  public pieChartData: SingleDataSet = this.items;
  public pieChartType: ChartType = 'pie';
  public pieChartLegend = true;
  public pieChartPlugins = [];
  public pieChartColors = [
    {
      backgroundColor: ['#ff6384', '#36a2eb', '#ffce56'],
    },
  ];

  constructor(private bikeService: BikeService) { 
    monkeyPatchChartJsTooltip();
    monkeyPatchChartJsLegend();
  }

  ngOnInit() {
    this.bikeService.getBikesGroupedByMake()
          .subscribe((result : BarChartData[]) => {
            result.forEach(x => {  
              this.makeNames.push(x.makeName);  
              this.items.push(x.items); 
            })  
          })
  }
}
