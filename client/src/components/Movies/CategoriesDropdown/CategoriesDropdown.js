import { Dropdown, ButtonGroup } from "react-bootstrap";

function CategoriesDropdown({
  currentCategoryName,
  categories,
  onCategorySelect,
}) {
  return (
    <Dropdown
      as={ButtonGroup}
      className="ms-3 mt-3"
      onSelect={onCategorySelect}
    >
      <Dropdown.Toggle className="btn btn-primary dropdown-toggle rounded-3 ps-3 pe-3">
        {currentCategoryName + " "}
      </Dropdown.Toggle>
      <Dropdown.Menu>
        {categories.map((x) => {
          if (x.name === currentCategoryName)
            return (
              <Dropdown.Item key={x.id} active>
                {x.name}
              </Dropdown.Item>
            );
          return <Dropdown.Item key={x.id}>{x.name}</Dropdown.Item>;
        })}
      </Dropdown.Menu>
    </Dropdown>
  );
}

export default CategoriesDropdown;
