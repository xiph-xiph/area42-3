import { Link } from "react-router-dom";
import "./BackButton.css";

type BackButtonProps = {
  to: string;
  label: string;
};

function BackButton({
  to,
  label,
}: BackButtonProps) {
  return (
    <Link
      to={to}
      className="back-button"
    >
      <span className="back-button-icon">←</span>

      <span>{label}</span>
    </Link>
  );
}

export default BackButton;  