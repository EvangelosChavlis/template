import Header from 'src/modules/shared/Header';
import { ButtonProps } from 'src/models/shared/buttonProps';
import { Badge, Col, Row, Tab, Tabs } from 'react-bootstrap';
import useView from 'src/modules/telemetry/pages/View/useView';
import { Link } from 'react-router-dom';

const View = () => {
  const { telemetry } = useView();

  const header = "Telemetry Info";

  const buttons: ButtonProps[] = [];

  return (
    <div className="container mt-4">
      <Header header={header} buttons={buttons} />
      <Row className="mt-2" style={{ flex: 1 }}>
        <Col style={{ display: "flex", flexDirection: "column" }}>
          <Tabs defaultActiveKey="general" id="telemetry-tabs" className="h-100">
            {/* General Info Tab */}
            <Tab
              eventKey="general"
              title={
                <span>
                  <i className="bi bi-info-circle-fill text-primary" /> General Info
                </span>
              }
            >
              <div
                className="p-3 border rounded bg-light"
                style={{ flex: 1, overflowY: "auto" }}
              >
                <div className="mt-4">
                  <strong className="strong-margin-right">
                    <i className="bi bi-arrow-right-circle-fill icon-margin-right" />
                    Method
                  </strong>
                  <span>{telemetry.method}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-file-earmark-code-fill icon-margin-right" />
                    Path
                  </strong>
                  <span>{telemetry.path}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-patch-check-fill icon-margin-right" />
                    Status Code
                  </strong>
                  <Badge bg="dark" pill >
                    {telemetry.statusCode}
                  </Badge>
                </div>
              </div>
            </Tab>

            {/* Performance Tab */}
            <Tab
              eventKey="performance"
              title={
                <span>
                  <i className="bi bi-speedometer2" /> Performance
                </span>
              }
            >
              <div
                className="p-3 border rounded bg-light"
                style={{ flex: 1, overflowY: "auto" }}
              >
                <div className="mt-4">
                  <strong className="strong-margin-right">
                    <i className="bi bi-speedometer2 icon-margin-right" />
                    Response Time
                  </strong>
                  <span>{telemetry.responseTime} ms</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-cpu-fill icon-margin-right" />
                    CPU Usage
                  </strong>
                  <span>{telemetry.cpUusage}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-memory icon-margin-right" />
                    Memory Used
                  </strong>
                  <span>{telemetry.memoryUsed} KB</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-cloud-upload-fill icon-margin-right" />
                    Request Body Size
                  </strong>
                  <span>{telemetry.requestBodySize} bytes</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-cloud-download-fill icon-margin-right" />
                    Response Body Size
                  </strong>
                  <span>{telemetry.responseBodySize} bytes</span>
                </div>
              </div>
            </Tab>

            {/* Metadata Tab */}
            <Tab
              eventKey="metadata"
              title={
                <span>
                  <i className="bi bi-geo-alt-fill" /> Metadata
                </span>
              }
            >
              <div
                className="p-3 border rounded bg-light"
                style={{ flex: 1, overflowY: "auto" }}
              >
                <div className="mt-4">
                  <strong className="strong-margin-right">
                    <i className="bi bi-clock-history icon-margin-right" />
                    Request Timestamp
                  </strong>
                  <Badge bg="primary">
                    {telemetry.requestTimestamp}
                  </Badge>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-clock-fill icon-margin-right" />
                    Response Timestamp
                  </strong>
                  <Badge bg="secondary">
                    {telemetry.responseTimestamp}
                  </Badge>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-geo-alt-fill icon-margin-right" />
                    Client IP
                  </strong>
                  <span >{telemetry.clientIp}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-browser-edge icon-margin-right" />
                    User Agent
                  </strong>
                  <span>{telemetry.userAgent}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-diagram-3-fill icon-margin-right" />
                    Thread ID
                  </strong>
                  <span >{telemetry.threadId}</span>
                </div>
                <div className="mt-5">
                  <strong className="strong-margin-right">
                    <i className="bi bi-person-fill icon-margin-right" />
                    User
                  </strong>
                  <span>
                    <Link to={`/users/${telemetry.userId}`} className="text-decoration-none">
                      {telemetry.userName}
                    </Link>
                  </span>
                </div>
              </div>
            </Tab>
          </Tabs>
        </Col>
      </Row>
    </div>
  );
};

export default View;
