import { Component, OnInit } from '@angular/core';
import { ShopService } from '../api/services/shop.service';
import { Product } from '../api/models/product';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})

export class ShopComponent implements OnInit {

  searchResult: Product[] = []
  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.shopService.searchShop({})
    .subscribe(r => this.searchResult = r, this.handleError)
  }

  private handleError(err: any) {
    console.log("Статус ошибки:", err.status)
    console.log("Текст ошибки:", err.statusText)
    console.log(err)
  }
}
