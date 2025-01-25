// packages
import { useState, useRef } from "react";
import Tab from "react-bootstrap/Tab";
import Tabs from "react-bootstrap/Tabs";
import Form from "react-bootstrap/Form";
import InputGroup from "react-bootstrap/InputGroup";
import Container from "react-bootstrap/Container";
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";

// Source
import Header from "src/modules/shared/Header";
import { ButtonProps } from "src/models/shared/buttonProps";
import ValidationErrorMessage from "src/modules/shared/ValidationErrorMessage";
import { Button } from "react-bootstrap";
import useUpdate from "src/modules/users/pages/Update/useUpdate";


const Update = () => {
  const { handleSubmit, user } = useUpdate();
  const [errors, setErrors] = useState<{
    firstName?: string;
    lastName?: string;
    email?: string;
    userName?: string;
    password?: string;

    address?: string;
    zipCode?: string;
    city?: string;
    state?: string;
    country?: string;
    phoneNumber?: string;
    mobilePhoneNumber?: string;

    bio?: string;
    dateOfBirth?: Date;
  }>({});

  const [activeTab, setActiveTab] = useState<string>("basic-info");
  const [showPassword, setShowPassword] = useState(false);

  const togglePasswordVisibility = () => {
    setShowPassword((prev) => !prev);
  };

  // Field refs
  const firstNameRef = useRef<HTMLInputElement>(null);
  const lastNameRef = useRef<HTMLInputElement>(null);
  const emailRef = useRef<HTMLInputElement>(null);
  const userNameRef = useRef<HTMLInputElement>(null);
  const passwordRef = useRef<HTMLInputElement>(null);
  const addressRef = useRef<HTMLInputElement>(null);
  const zipCodeRef = useRef<HTMLInputElement>(null);
  const cityRef = useRef<HTMLInputElement>(null);
  const stateRef = useRef<HTMLInputElement>(null);
  const countryRef = useRef<HTMLInputElement>(null);
  const phoneNumberRef = useRef<HTMLInputElement>(null);
  const mobilePhoneNumberRef = useRef<HTMLInputElement>(null);
  const bioRef = useRef<HTMLTextAreaElement>(null);
  const dateOfBirthRef = useRef<HTMLInputElement>(null);

  const header = "Create User";

  const onSubmit = async () => {
    try {
      setErrors({});
      await handleSubmit({
        preventDefault: () => {},
        firstName: firstNameRef.current?.value ?? "",
        lastName: lastNameRef.current?.value ?? "",
        email: emailRef.current?.value ?? "",
        userName: userNameRef.current?.value ?? "",
        password: passwordRef.current?.value ?? "",
        address: addressRef.current?.value ?? "",
        zipCode: zipCodeRef.current?.value ?? "",
        city: cityRef.current?.value ?? "",
        state: stateRef.current?.value ?? "",
        country: countryRef.current?.value ?? "",
        phoneNumber: phoneNumberRef.current?.value ?? "",
        mobilePhoneNumber: mobilePhoneNumberRef.current?.value ?? "",
        bio: bioRef.current?.value ?? "",
        dateOfBirth: new Date(dateOfBirthRef.current?.value!),
      });
    } catch (validationErrors) {
      if (typeof validationErrors === "object") {
        setErrors(validationErrors!);
      }
    }
  };

  const buttons: ButtonProps[] = [
    {
      title: "Submit",
      action: onSubmit,
      icon: <i className="bi bi-check"></i>,
      color: "success",
      placement: "top",
      disabled: false,
    },
  ];


  return (
    <div className="container mt-4 mb-3">
      <Header header={header} buttons={buttons} />
      <Row className="mt-2" style={{ flex: 1 }}>
        <Col style={{ display: "flex", flexDirection: "column" }}>
          <Tabs
            defaultActiveKey="basic-info"
            activeKey={activeTab}
            onSelect={(k) => setActiveTab(k || "basic-info")}
            className="mt-3"
          >
            <Tab 
              eventKey="basic-info" 
              title={
                <>
                  <i className="bi bi-person-circle"/> Basic Info
                </>
              }
              className="p-3 border rounded"
              style={{ flex: 1, overflowY: "auto" }}
            >
              <Container className="mt-2">
                <Form>
                  <InputGroup className="mt-2">
                    <InputGroup.Text>
                      <i className="bi bi-person" />
                    </InputGroup.Text>
                    <Form.Control
                      name="firstName"
                      defaultValue={user.firstName}
                      ref={firstNameRef}
                      placeholder="First name ..."
                      className={errors.firstName ? "is-invalid" : ""}
                    />
                  </InputGroup>
                  <ValidationErrorMessage message={errors.firstName} />

                  <InputGroup className="mt-4">
                    <InputGroup.Text>
                      <i className="bi bi-person-fill" />
                    </InputGroup.Text>
                    <Form.Control
                      name="lastName"
                      defaultValue={user.lastName}
                      ref={lastNameRef}
                      placeholder="Last name ..."
                      className={errors.lastName ? "is-invalid" : ""}
                    />
                  </InputGroup>
                  <ValidationErrorMessage message={errors.lastName} />

                  <InputGroup className="mt-4">
                    <InputGroup.Text>
                      <i className="bi bi-envelope" />
                    </InputGroup.Text>
                    <Form.Control
                      name="email"
                      defaultValue={user.email}
                      ref={emailRef}
                      type="email"
                      placeholder="Email ..."
                      className={errors.email ? "is-invalid" : ""}
                    />
                  </InputGroup>
                  <ValidationErrorMessage message={errors.email} />

                  <InputGroup className="mt-4">
                    <InputGroup.Text>
                      <i className="bi bi-person-badge" />
                    </InputGroup.Text>
                    <Form.Control
                      name="userName"
                      defaultValue={user.userName}
                      ref={userNameRef}
                      placeholder="User name ..."
                      className={errors.userName ? "is-invalid" : ""}
                    />
                  </InputGroup>
                  <ValidationErrorMessage message={errors.userName} />

                  <InputGroup className="mt-4">
                    <InputGroup.Text>
                      <i className="bi bi-lock" />
                    </InputGroup.Text>
                    <Form.Control
                      name="password"
                      defaultValue={user.initialPassword}
                      ref={passwordRef}
                      type={showPassword ? "text" : "password"}
                      placeholder="Password ..."
                      className={errors.password ? "is-invalid" : ""}
                    />
                    <InputGroup.Text>
                      <Button
                        variant="outline-secondary"
                        onClick={togglePasswordVisibility}
                        className="border-0"
                      >
                        {showPassword ? (
                          <i className="bi bi-eye-slash" />
                        ) : (
                          <i className="bi bi-eye" />
                        )}
                      </Button>
                    </InputGroup.Text>
                  </InputGroup>
                  <ValidationErrorMessage message={errors.password} />
                </Form>
              </Container>
            </Tab>
            <Tab 
              eventKey="contact-info" 
              title={
                <>
                  <i className="bi bi-telephone-fill"/> Contact Info
                </>
              }
              className="p-3 border rounded"
              style={{ flex: 1, overflowY: "auto" }}
            >
              <Container className="mt-4">
                <Form>
                  <InputGroup className="mt-2">
                    <InputGroup.Text>
                      <i className="bi bi-telephone" />
                    </InputGroup.Text>
                    <Form.Control
                      name="phoneNumber"
                      defaultValue={user.phoneNumber}
                      ref={phoneNumberRef}
                      placeholder="Phone number ..."
                      className={errors.country ? "is-invalid" : ""}
                    />
                  </InputGroup>
                  <ValidationErrorMessage message={errors.phoneNumber} />

                  <InputGroup className="mt-4">
                    <InputGroup.Text>
                      <i className="bi bi-phone" />
                    </InputGroup.Text>
                    <Form.Control
                      name="mobilePhoneNumber"
                      defaultValue={user.mobilePhoneNumber}
                      ref={mobilePhoneNumberRef}
                      placeholder="Mobile phone number ..."
                      className={errors.country ? "is-invalid" : ""}
                    />
                  </InputGroup>
                  <ValidationErrorMessage message={errors.mobilePhoneNumber} />

                  <InputGroup className="mt-4">
                    <InputGroup.Text>
                      <i className="bi bi-globe" />
                    </InputGroup.Text>
                    <Form.Control
                      name="country"
                      defaultValue={user.country}
                      ref={countryRef}
                      placeholder="Country ..."
                      className={errors.country ? "is-invalid" : ""}
                    />
                  </InputGroup>
                  <ValidationErrorMessage message={errors.country} />

                  <InputGroup className="mt-4">
                    <InputGroup.Text>
                      <i className="bi bi-map" />
                    </InputGroup.Text>
                    <Form.Control
                      name="state"
                      defaultValue={user.state}
                      ref={stateRef}
                      placeholder="State ..."
                      className={errors.state ? "is-invalid" : ""}
                    />
                  </InputGroup>
                  <ValidationErrorMessage message={errors.state} />

                  <InputGroup className="mt-4">
                    <InputGroup.Text>
                      <i className="bi bi-geo-alt" />
                    </InputGroup.Text>
                    <Form.Control
                      name="city"
                      defaultValue={user.city}
                      ref={cityRef}
                      placeholder="City ..."
                      className={errors.city ? "is-invalid" : ""}
                    />
                  </InputGroup>
                  <ValidationErrorMessage message={errors.city} />

                  <InputGroup className="mt-4">
                    <InputGroup.Text>
                      <i className="bi bi-house" />
                    </InputGroup.Text>
                    <Form.Control
                      name="address"
                      defaultValue={user.address}
                      ref={addressRef}
                      placeholder="Address ..."
                      className={errors.address ? "is-invalid" : ""}
                    />
                  </InputGroup>
                  <ValidationErrorMessage message={errors.address} />

                  <InputGroup className="mt-4">
                    <InputGroup.Text>
                      <i className="bi bi-123" />
                    </InputGroup.Text>
                    <Form.Control
                      name="zipCode"
                      defaultValue={user.zipCode}
                      ref={zipCodeRef}
                      placeholder="Zip code ..."
                      className={errors.zipCode ? "is-invalid" : ""}
                    />
                  </InputGroup>
                  <ValidationErrorMessage message={errors.zipCode} />
                </Form>
              </Container>
            </Tab>
            <Tab 
              eventKey="additional-info" 
              title={
                <>
                  <i className="bi bi-info-circle"></i> Additional Info
                </>
              }
              className="p-3 border rounded"
              style={{ flex: 1, overflowY: "auto" }}
            >
              <Container className="mt-4">
                <Form>
                  <InputGroup className="mt-2">
                    <InputGroup.Text>
                      <i className="bi bi-calendar-date"></i>
                    </InputGroup.Text>
                    <Form.Control
                      name="dateOfBirth"
                      ref={dateOfBirthRef}
                      defaultValue={user.dateOfBirth}
                      type="date"
                      placeholder="Select date of birth"
                      className={errors.dateOfBirth ? "is-invalid" : ""}
                    />
                  </InputGroup>
                  <ValidationErrorMessage message={errors.dateOfBirth instanceof Date ? errors.dateOfBirth.toDateString() : errors.dateOfBirth} />

                  <InputGroup className="mt-4">
                    <InputGroup.Text>
                      <i className="bi bi-pencil"></i>
                    </InputGroup.Text>
                    <Form.Control
                      name="bio"
                      defaultValue={user.bio}
                      ref={bioRef}
                      as="textarea"
                      placeholder="Bio ..."
                      className={errors.bio ? "is-invalid" : ""}
                    />
                  </InputGroup>
                  <ValidationErrorMessage message={errors.bio} />
                </Form>
              </Container>
            </Tab>
          </Tabs>
        </Col>
      </Row>
    </div>
  );
};

export default Update;
