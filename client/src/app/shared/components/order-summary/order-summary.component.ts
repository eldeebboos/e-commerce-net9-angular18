import { CurrencyPipe, NgIf } from '@angular/common';
import { Component, inject } from '@angular/core';
import { CartService } from '../../../core/services/cart.service';
import { MatButton } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatInput } from '@angular/material/input';

@Component({
  selector: 'app-order-summary',
  standalone: true,
  imports: [
    NgIf,
    FormsModule,
    CurrencyPipe,
    RouterLink,
    MatButton,
    MatLabel,
    MatIcon,
    MatFormField,
    MatInput,
  ],
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.scss',
})
export class OrderSummaryComponent {
  removeCouponCode() {
    throw new Error('Method not implemented.');
  }
  applyCouponCode() {
    throw new Error('Method not implemented.');
  }
  cartService = inject(CartService);
}
