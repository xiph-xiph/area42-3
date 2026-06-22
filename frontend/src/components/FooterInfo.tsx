import "./FooterInfo.css";

type FooterInfoProps = {
  businessName: string;
  address: string;
  postalCode: string;
  phone: string;
  openingDays: string;
  openingHours: string;
  socialLogo: string;
};

function FooterInfo({
  businessName,
  address,
  postalCode,
  phone,
  openingDays,
  openingHours,
  socialLogo,
}: FooterInfoProps) {
  return (
    <footer className="footer-info">
      <div className="footer-section">
        <h3>Openingstijden</h3>

        <p>{openingDays}</p>
        <p>{openingHours}</p>
      </div>

      <div className="footer-section">
        <p>{businessName}</p>
        <p>{address}</p>
        <p>{postalCode}</p>
        <p>{phone}</p>
      </div>

      <div className="footer-instagram">
        <img src={socialLogo} alt="instagram" />
      </div>
    </footer>
  );
}

export default FooterInfo;
