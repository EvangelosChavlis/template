// packages
import Container from "react-bootstrap/Container";

const Footer = () => {
  return (
    <footer className="bg-primary text-light mt-auto">
      <Container className="text-center">
          Evangelos Chavlis © {new Date().getFullYear()} Weather App.
          <a
            href="https://www.linkedin.com/in/vagelis-chavlis/"
            target="_blank"
            rel="noopener noreferrer"
            className="text-light mx-2"
          >
            <i className="bi bi-linkedin" style={{ fontSize: "1.5rem" }}></i>
          </a>
          <a
            href="https://github.com/EvangelosChavlis"
            target="_blank"
            rel="noopener noreferrer"
            className="text-light mx-2"
          >
            <i className="bi bi-github" style={{ fontSize: "1.5rem" }}></i>
          </a>
      </Container>
    </footer>
  );
};

export default Footer;
