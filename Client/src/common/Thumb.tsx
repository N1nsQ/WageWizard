import { API_BASE } from "../config";
import "../App.css";
import { useTranslation } from "react-i18next";

interface ThumbProps {
  imageUrl: string | undefined | null;
  alt?: string;
}

const Thumb = ({ imageUrl, alt }: ThumbProps) => {
  const { t } = useTranslation();
  const defaultImg = "/default.png";
  const thumbSize = "?width=50&height=50";
  const src = imageUrl ? `${API_BASE}${imageUrl}${thumbSize}` : defaultImg;

  const altText = imageUrl
    ? alt && alt.trim() !== ""
      ? alt
      : t("altTextNotAvailable")
    : t("imageNotAvailable");

  return <img className="thumb-image" src={src} alt={altText} />;
};

export default Thumb;
