import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FlightService } from './../api/services/flight.service';
import { FlightRm } from '../api/models/flight-rm';
import { AuthService } from '.././auth/auth.service';
import {FormBuilder, Validators } from '@angular/forms'
import { BookDto } from '../api/models';
import { json } from 'node:stream/consumers';
@Component({
  selector: 'app-book-flights',
  templateUrl: './book-flights.component.html',
  styleUrls: ['./book-flights.component.css']
})
export class BookFlightsComponent implements OnInit{
  
  constructor(private route: ActivatedRoute,
    private router: Router,
    private flightService: FlightService,
    private authService: AuthService,
    private fb: FormBuilder) { }

  flightId: string = 'not loaded'
  flight: FlightRm = {}

  form = this.fb.group({
    number: [1, Validators.compose([Validators.required, Validators.min(1),Validators.max(254)])] 
  })

  ngOnInit(): void {
    

    this.route.paramMap.subscribe(p => this.findFlight(p.get("flightId")))
  }

  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? 'not passed';

    this.flightService.findFlight({ id: this.flightId })
      .subscribe(flight => this.flight = flight, this.handleError)
  }

  private handleError = (err: any) => {
    if (err.status == 404 || err.status == 400) {
      alert("Рейс не найден")
        this.router.navigate(['/search-flight'])
    }

    if (err.status == 409) {
      console.log(err)
      alert("Не хватает мест")
    }

    console.log("Статус ошибки:", err.status)
    console.log("Текст ошибки:", err.statusText)
    console.log(err)
  }

  book() {

    if (this.form.invalid)
      return;

    console.log(`Забронировано ${this.form.get('number')?.value} мест на рейс: ${this.flight.id}`);

    const numberValue = this.form.get('number')?.value;
    const numberOfSeats: number | undefined = (numberValue !== null && numberValue !== undefined) ? numberValue : undefined;

    const booking:BookDto = {
      flightId: this.flight.id,
      passengerEmail: this.authService.currentUser?.email,
      numberOfSeats: numberOfSeats
    }

    this.flightService.bookFlight({ body: booking }).
      subscribe(_ => this.router.navigate(['/my-booking']), this.handleError)
  }

  get number() {
    return this.form.controls.number
  }
}

