import "./area54.css";

import area54Logo from "../assets/images/area54_logo.png";

import heroFoto54 from "../assets/images/area54_heropng.png";



import ServiceCard from "../components/ServiceCard";

import famigliaLogo from "../assets/logos/frame_logo_lafamiglia.png";

import famigliaImage from "../assets/images/area54_lafamiglia_background.png";

import snackhoekLogo from "../assets/logos/frame_snackbar_logo.png";

import snackhoekImage from "../assets/images/snackhoek_hero.png";



const services = [
    {
        id: 1,
        logo: famigliaLogo,
        image: famigliaImage,
        route: "/restaurant",
    },
    {
        id: 2,
        logo: snackhoekLogo,
        image:snackhoekImage,
        route:"/snackhoek",
    }
]



const HomePageNew = () => {
    return (
        
                <div className="area54-container">
                
                    <div className="logo54-container"><img src={area54Logo} alt="logo" className="logo-area54" /></div>
                
                    <div className="hero-container">
                        <img src={heroFoto54} className="herofoto" alt="herofoto" />
                    </div>
                    
                    
                    <p className="welcome-text">Welkom bij vakantiepark Area54!De ideale bestemming om te ontspannen, plezier te maken en samen mooie herinneringen te creëren.Wij wensen je een fijn verblijf op ons park.</p>
                
                    <div className="greenline"></div>

                    <div className="text-area54">
                        <h3>Restaurant</h3>
                        <p>Tijdens je verblijf op ons vakantiepark kun je genieten van diverse eetgelegenheden. Ontdek ons aanbod aan restaurants, snacks en drankjes en kies waar jij vandaag zin in hebt. We wensen je een smakelijk verblijf!</p>
                    </div>

                    <section className="restaurant-area54">
                        {services.map((service) => (
                                <ServiceCard 
                                key={service.id}
                                logo={service.logo}
                                image={service.image}
                                route={service.route}
                                
                                />
                        ))}
                    </section>
                    
                    <div className="greenline"></div>
                
                </div>
        
        
    )
}

export default HomePageNew;