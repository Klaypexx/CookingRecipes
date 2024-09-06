import React from 'react';
import LinkBlockProps from '../../../Types/LinkBlockProps';
import Link from '../BaseLink/BaseLink';
import styles from './LinkBlock.module.css';

const LinkBlock: React.FC<LinkBlockProps> = ({
  linkPrimaryText,
  linkSecondaryText,
  onClickPrimary,
  onClickSecondary,
}) => {
  return (
    <div className={styles.linkBlock}>
      <Link primary text={linkPrimaryText} onClick={onClickPrimary}></Link>
      <Link text={linkSecondaryText} onClick={onClickSecondary}></Link>
    </div>
  );
};

export default LinkBlock;
