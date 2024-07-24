import styles from "./BaseCard.module.css"

interface CardProps {
    children?: React.ReactNode;
}

const BaseCard: React.FC<CardProps> = ({ children }) => {
    return (
        <>
            <div className={styles.cardContainer}>
                    <div className={styles.cardBackground}></div>
                    <div className={styles.cardPhoto}>
                    </div>
                    <div className={styles.cardInformation}>
                        {children}
                    </div>
            </div>
        </>
    )
}

export default BaseCard;