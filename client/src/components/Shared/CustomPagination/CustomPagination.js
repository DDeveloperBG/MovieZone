import { Pagination } from "react-bootstrap";

function PaginationTemplate({ page, pageChangedHandler }) {
  return (
    <Pagination>
      <Pagination.First />

      <Pagination.Item onClick={pageChangedHandler}>{page - 1}</Pagination.Item>
      <Pagination.Item active onClick={pageChangedHandler}>
        {page}
      </Pagination.Item>
      <Pagination.Item onClick={pageChangedHandler}>{page + 1}</Pagination.Item>

      <Pagination.Last />
    </Pagination>
  );
}

export default PaginationTemplate;
