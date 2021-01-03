import { MatPaginatorIntl } from "@angular/material/paginator";

export function CustomPaginator() {
  const customPaginatorIntl = new MatPaginatorIntl();

  customPaginatorIntl.itemsPerPageLabel = 'Registros por página:';
  customPaginatorIntl.nextPageLabel = 'Próxima Página';
  customPaginatorIntl.previousPageLabel = 'Próxima Anterior';
  customPaginatorIntl.firstPageLabel = 'Primeira Página';
  customPaginatorIntl.lastPageLabel = 'Última Página';

  let getRangeLabel = (page: number, pageSize: number, length: number) => {
    return ((page * pageSize) + 1) + ' - ' + ((page * pageSize) + pageSize) + ' de ' + length;
  }
  
  customPaginatorIntl.getRangeLabel = getRangeLabel;

  return customPaginatorIntl;
}
