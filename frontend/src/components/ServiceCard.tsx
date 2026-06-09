import { Link } from "react-router-dom";
import "./ServiceCard.css";

type ServiceCardProps = {
  logo: string;
  image: string;
  route: string;
};

const ServiceCard = ({
  logo,
  image,
  route,
}: ServiceCardProps) => {
  return (
    <Link to={route} className="service-card">
      <img
        src={image}
        alt=""
        className="service-card-image"
      />

      <img
        src={logo}
        alt=""
        className="service-card-logo"
      />
    </Link>
  );
};

export default ServiceCard;