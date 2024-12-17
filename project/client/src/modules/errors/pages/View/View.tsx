// packages
import Badge from 'react-bootstrap/Badge';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';

// source
import useView from 'src/modules/errors/pages/View/useView';
import Header from 'src/modules/shared/Header';
import { ButtonProps } from 'src/models/shared/buttonProps';
import LoadingSpinner from 'src/modules/shared/LoadingSpinner';

const View = () => {
  const { error } = useView();

  if (!error) return <LoadingSpinner />
  const header = "Error Info";

  const buttons: ButtonProps[] = [
  ];

  return (
    <div className="container mt-4">
      <Header 
        header={header}  
        buttons={buttons} 
      />

      <Row className="mt-4" style={{ flex: 1 }}>
        <Col style={{ display: "flex", flexDirection: "column" }}>
          <Tabs defaultActiveKey="details" id="user-tabs" className="h-100">
            <Tab
              eventKey="details"
              title={
                <span>
                  <i className="bi bi-info-circle-fill"></i> Info
                </span>
              }
            >
              <div
                className="p-3 border rounded bg-light"
                style={{ flex: 1, overflowY: "auto" }}
              >
                <div className="mt-3">
                  <strong className="strong-margin-right">
                    <i className="bi bi-exclamation-circle-fill icon-margin-right" />Error Message
                  </strong>
                  <span>{error.error}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-info-circle icon-margin-right" />Status Code
                  </strong>
                  <Badge bg="dark" pill>
                    {error.statusCode}
                  </Badge>
                </div>
                <hr/>
                <div className="mt-4">
                  <strong className="strong-margin-right">
                    <i className="bi bi-file-earmark-text icon-margin-right" />Instance
                  </strong>
                  <span>{error.instance}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-shield-exclamation icon-margin-right" />Exception Type
                  </strong>
                  <span>{error.exceptionType}</span>
                </div>
                <hr/>
                <div className="mt-4">
                  <strong className="strong-margin-right">
                    <i className="bi bi-calendar-check icon-margin-right" />Timestamp
                  </strong>
                  <Badge bg="primary">{error.timestamp}</Badge>
                </div>
              </div>
            </Tab>
            <Tab
              eventKey="contact-info"
              title={
                <span>
                  <i className="bi bi-code-slash"></i> Stack Trace
                </span>
              }
            >
              <div
                className="p-3 border rounded bg-light"
                style={{ flex: 1, overflowY: "auto" }}
              >
                <pre className="bg-dark text-light p-2 rounded">{error.stackTrace}</pre>
              </div>
            </Tab>
          </Tabs>
        </Col>
      </Row>
    </div>
  );
};

export default View;
