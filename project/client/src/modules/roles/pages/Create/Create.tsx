// packages
import { useRef, useState } from "react";
import Form from "react-bootstrap/Form";
import InputGroup from "react-bootstrap/InputGroup";
import Container from "react-bootstrap/Container";

// source
import Header from "src/modules/shared/Header";
import { ButtonProps } from "src/models/shared/buttonProps";
import useCreate from "src/modules/roles/pages/Create/useCreate";
import ValidationErrorMessage from "src/modules/shared/ValidationErrorMessage";

const Create = () => {
  const { handleSubmit } = useCreate();
  const [errors, setErrors] = useState<{ name?: string; description?: string }>({});
  const nameRef = useRef<HTMLInputElement>(null);
  const descriptionRef = useRef<HTMLTextAreaElement>(null);

  const header = "Create Role";

  const onSubmit = async () => {
    try {
      setErrors({});
      
      await handleSubmit({
        preventDefault: () => {},
        name: nameRef.current?.value ?? "",
        description: descriptionRef.current?.value ?? "",
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
    <div className="container mt-4">
      <Header header={header} buttons={buttons} />
      <div
        className="p-3 border rounded mt-2"
        style={{ flex: 1, overflowY: "auto" }}
      >
        <Container className="mt-4">
          <Form
            onSubmit={(e) => {
              e.preventDefault();
              onSubmit();
            }}
          >
            <InputGroup className="mb-3 mt-2">
              <InputGroup.Text>
                <i className="bi bi-chat"></i>
              </InputGroup.Text>
              <InputGroup.Text id="name">Name</InputGroup.Text>
              <Form.Control
                name="name"
                ref={nameRef}
                placeholder="Role name..."
                aria-label="name"
                aria-describedby="name"
                className={errors.name ? "is-invalid" : "is-valid"}
                required
              />
            </InputGroup>
            <ValidationErrorMessage message={errors.name} />

            <InputGroup className="mb-3 mt-5">
              <InputGroup.Text>
                <i className="bi bi-chat-dots"></i>
              </InputGroup.Text>
              <InputGroup.Text id="description">Description</InputGroup.Text>
              <Form.Control
                name="description"
                ref={descriptionRef}
                as="textarea"
                placeholder="Role description..."
                aria-label="description"
                className={errors.description ? "is-invalid" : "is-valid"}
                required
              />
            </InputGroup>
            <ValidationErrorMessage message={errors.description} />
          </Form>
        </Container>
      </div>
    </div>
  );
};

export default Create;
