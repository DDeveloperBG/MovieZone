import { Pagination } from "react-bootstrap";

function PaginationTemplate({ page, allPagesCount, pageChangedHandler }) {
  return (
    <Pagination>
      <Pagination.First onClick={pageChangedHandler.bind(null, 1)} />

      {page > 1 ? (
        <Pagination.Item onClick={pageChangedHandler}>
          {page - 1}
        </Pagination.Item>
      ) : null}

      <Pagination.Item active onClick={pageChangedHandler}>
        {page}
      </Pagination.Item>

      {page < allPagesCount ? (
        <Pagination.Item onClick={pageChangedHandler}>
          {page + 1}
        </Pagination.Item>
      ) : null}

      <Pagination.Last
        onClick={pageChangedHandler.bind(
          null,
          allPagesCount > 0 ? allPagesCount : 1
        )}
      />
    </Pagination>
  );
}

export default PaginationTemplate;
