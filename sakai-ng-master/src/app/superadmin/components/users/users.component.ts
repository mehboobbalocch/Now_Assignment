import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/demo/api/customer';
import { CustomerService } from 'src/app/demo/service/customer.service';
import { ProductService } from 'src/app/demo/service/product.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent implements OnInit {
  customers3: Customer[] = [];
  
  constructor(private customerService: CustomerService, private productService: ProductService) { }

  ngOnInit() {
    this.customerService.getCustomersLarge().then(customers => this.customers3 = customers);
  }

}
