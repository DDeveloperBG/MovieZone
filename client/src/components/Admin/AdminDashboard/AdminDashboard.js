import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";

import "./AdminDashboard.scss";

function AdminDashboard() {
  return (
    <div id="admin-dashboard-wrapper" className="container mt-2">
      <h1 className="text-center">Administration Dashboard</h1>
      <div>
        <ul className="h4">
          <Link to="/admin/addMovie">
            <Button>Add Movie</Button>
          </Link>
        </ul>
      </div>
    </div>
  );
}

export default AdminDashboard;
