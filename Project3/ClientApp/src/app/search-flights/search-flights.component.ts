import { Component, OnInit } from '@angular/core';
import { FlightService } from './../api/services/flight.service';
import { FlightRm } from '../api/models/flight-rm';
import { TimePlaceRm } from '../api/models/time-place-rm';
import {FormBuilder } from '@angular/forms'
import { SearchFlight$Params } from '../api/fn/flight/search-flight';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent implements OnInit {

  searchResult: FlightRm[] = []

  constructor(private flightService: FlightService,
    private fb: FormBuilder) { }

  searchForm = this.fb.group({
    From: [''],
    Destination: [''],
    FromDate: [''],
    ToDate: [''],
    numberOfPassenger: [1]
  })

  ngOnInit(): void {
    this.flightService.searchFlight({})
      .subscribe(r => this.searchResult = r, this.handleError)
  }

  search() {
    console.log(this.searchForm.value)
    this.flightService.searchFlight(this.searchForm.value as SearchFlight$Params)
    .subscribe(r => this.searchResult = r, this.handleError)
  }
  private handleError(err: any) {
    console.log("Статус ошибки:", err.status)
    console.log("Текст ошибки:", err.statusText)
    console.log(err)
  }
}

