import FormComponentError from "../FormComponentError/FormComponentError";
import { HiShieldExclamation } from "react-icons/hi";

function FormComponentErrors({ children: errorMessages }) {
  if (typeof errorMessages === "string") {
    errorMessages = [errorMessages];
  }

  return (
    <div className="mt-1">
      {errorMessages.map((x) => (
        <p key={x} className="mb-0 p-1">
          <HiShieldExclamation style={{ color: "#F53838" }} className="me-2" />
          <FormComponentError message={x} />
        </p>
      ))}
    </div>
  );
}

export default FormComponentErrors;
