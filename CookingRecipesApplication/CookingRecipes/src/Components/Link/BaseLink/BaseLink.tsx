import React from 'react';
import { Link } from 'react-router-dom';
import styles from './BaseLink.module.css';
import classNames from 'classnames';
import LinkProps from '../../../Types/LinkProps';

const BaseLink: React.FC<LinkProps> = ({ primary, to, newStyle, className, linkText, onClick, children }) => {
  const LinkclassName = classNames(!primary && styles.linkSecondary, styles.baseLink);

  const textClassName = classNames(primary ? styles.textPrimary : styles.textSecondary, styles.baseText);

  const styleList = {
    ...newStyle,
    ...(primary && { backgroundColor: 'rgb(253, 177, 0)' }),
  };

  return (
    <Link
      to={to ? to : location.pathname}
      className={classNames(LinkclassName, className)}
      state={{ from: location.pathname }}
      style={styleList}
      onClick={onClick}
    >
      {children}
      {linkText ? <p className={textClassName}>{linkText}</p> : null}
    </Link>
  );
};

export default BaseLink;
