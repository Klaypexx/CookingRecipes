import React from 'react';
import { Link } from 'react-router-dom';
import styles from './BaseLink.module.css';
import classNames from 'classnames';
import LinkProps from '../../../Types/LinkProps';

const BaseLink: React.FC<LinkProps> = ({ primary, navigation, newStyle, className, linkText, onClick, children }) => {
  const LinkclassName = classNames(primary ? undefined : styles.linkSecondary, styles.baseLink);

  const textClassName = classNames(primary ? styles.textPrimary : styles.textSecondary, styles.baseText);

  const styleList = {
    ...newStyle,
    ...(primary ? { backgroundColor: 'rgb(253, 177, 0)' } : undefined),
  };

  return (
    <Link
      to={navigation ? navigation : location.pathname}
      state={{ from: location.pathname }}
      className={classNames(LinkclassName, className)}
      style={styleList}
      onClick={onClick}
    >
      {children}
      {linkText ? <p className={textClassName}>{linkText}</p> : null}
    </Link>
  );
};

export default BaseLink;
