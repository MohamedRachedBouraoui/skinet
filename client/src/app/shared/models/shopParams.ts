export class ShopParams {

  brandId = 0;
  productTypeId = 0;
  sortBy = 'name';

  pageIndex = 1; // like in the server
  pageSize = 6;


  serverTotalProductsCount = 0;
  searchBy: string;

  get pagingHeaderPart1(): number {
    return (this.pageIndex - 1) * this.pageSize + 1;
  }

  get pagingHeaderPart2(): number {
    return (this.pageIndex * this.pageSize) > this.serverTotalProductsCount ?
      this.serverTotalProductsCount :
      (this.pageIndex * this.pageSize);
  }

}
