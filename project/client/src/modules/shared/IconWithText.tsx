// packages
import { Fragment } from 'react';

// sources
import { IconWithTextProps } from 'src/models/shared/iconWithTextProps';

const IconWithText = ({ iconClass, text }: IconWithTextProps) => {
  return (
    <Fragment>
      <i className={iconClass} />{" "}
      <span>{text}</span>
    </Fragment>
  );
};

export default IconWithText;
